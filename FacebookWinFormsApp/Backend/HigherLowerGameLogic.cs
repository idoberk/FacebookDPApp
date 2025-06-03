using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public delegate void PagesSelectedEventHandler(object sender, PageSelectedEventArgs e);
    public delegate void TimerTickEventHandler(object sender, TimerEventArgs e);
    public delegate void TimeExpiredEventHandler(object sender, EventArgs e);

    public class HigherLowerGameLogic : GameTemplate
    {
        private const int k_MinPagesRequired = 2;

        private readonly User r_LoggedInUser;
        private readonly GameConfiguration r_GameConfiguration;
        private readonly List<MockPage> r_UnusedPages = new List<MockPage>();
        private readonly HashSet<string> r_UsedPageIds = new HashSet<string>();

        private MockPage m_CurrentWinningPage;
        private MockPage m_NewChallengingPage;
        private static readonly Random sr_RandomPage = new Random();
        private int m_RemainingSeconds;
        private bool m_IsTimerRunning;
        private bool m_LastGuessResult;
        private int m_WrongAnswersCount;

        public event PagesSelectedEventHandler PagesSelected;
        public event TimerTickEventHandler TimerTick;
        public event TimeExpiredEventHandler TimeExpired;

        public int TimeLimit { get; private set; }

        public HigherLowerGameLogic(User i_LoggedInUser) : this(i_LoggedInUser, eGameMode.Easy)
        {
            
        }

        public HigherLowerGameLogic(User i_LoggedInUser, eGameMode i_GameMode) : base()
        {
            r_LoggedInUser = i_LoggedInUser;
            r_GameConfiguration = GameConfiguration.CreateGameConfiguration(i_GameMode);
            TimeLimit = r_GameConfiguration.InitialTimeSeconds;
            m_RemainingSeconds = TimeLimit;
            m_IsTimerRunning = false;
            m_WrongAnswersCount = 0;
        }

        protected override void InitializeGame()
        {
            r_UnusedPages.Clear();
            r_UsedPageIds.Clear();
            m_WrongAnswersCount = 0;
            TimeLimit = r_GameConfiguration.InitialTimeSeconds;

            initPagesList();
            m_CurrentWinningPage = getNextPage();
            m_NewChallengingPage = getNextPage();
        }

        protected override bool PrepareRound()
        {
            if(m_CurrentWinningPage == null || m_NewChallengingPage == null)
            {
                return false;
            }

            //MockPage nextPage = getNextPage();

            //if(nextPage == null && r_UnusedPages.Count == 0)
            //{
            //    return false;
            //}

            return true;
        }

        protected override void DisplayRound()
        {
            OnPagesSelected(
                new PageSelectedEventArgs(
                    m_CurrentWinningPage.GetOriginalPage(),
                    m_NewChallengingPage.GetOriginalPage()));
        }

        protected override void StartTimer()
        {
            m_RemainingSeconds = TimeLimit;
            m_IsTimerRunning = true;
            OnTimerTick();
        }

        protected override void stopTimer()
        {
            m_IsTimerRunning = false;
        }

        protected override GuessResultEventArgs EvaluateGuess(bool i_UserGuess)
        {
            bool isFirstPageHigher = m_CurrentWinningPage.LikesCount > m_NewChallengingPage.LikesCount;
            bool isCorrect = (i_UserGuess == isFirstPageHigher);

            m_LastGuessResult = isCorrect;

            if(!isCorrect)
            {
                m_WrongAnswersCount++;

                if(r_GameConfiguration.ResetTimeOnWrongAnswer)
                {
                    TimeLimit = r_GameConfiguration.InitialTimeSeconds;
                }
            }

            return new GuessResultEventArgs(
                (int)m_CurrentWinningPage.LikesCount,
                (int)m_NewChallengingPage.LikesCount,
                isCorrect,
                isFirstPageHigher);
        }

        protected override void UpdateScore()
        {
            int pointsToAdd = r_GameConfiguration.PointsPerCorrectAnswer;

            if(r_GameConfiguration.EnableTimeBonus)
            {
                pointsToAdd += m_RemainingSeconds * r_GameConfiguration.TimeBonusMultiplier;
            }

            Score += pointsToAdd;

            if(r_GameConfiguration.DecreaseTimeEachRound)
            {
                TimeLimit = Math.Max(
                    r_GameConfiguration.MinTimeSeconds,
                    TimeLimit - r_GameConfiguration.TimeDecreaseAmount);
            }
        }

        protected override void CheckGameStatus()
        {
            bool hasReachedMaxWrongAnswers = r_GameConfiguration.MaxWrongAnswers > 0 &&
                                            m_WrongAnswersCount >= r_GameConfiguration.MaxWrongAnswers;

            if(hasReachedMaxWrongAnswers)
            {
                IsGameOver = true;
                OnGameOver(new GameOverEventArgs(Score));
            }
        }

        protected override bool GetDefaultGuess()
        {
            return true;
        }

        protected override void HandleRoundError(Exception i_Exception)
        {
            throw new Exception("Error during game round: " + i_Exception.Message, i_Exception);
        }

        public void TimerTicks()
        {
            if(m_IsTimerRunning && !IsGameOver)
            {
                m_RemainingSeconds--;

                if(m_RemainingSeconds <= 0)
                {
                    m_RemainingSeconds = 0;
                    m_IsTimerRunning = false;
                    OnTimeExpired();
                } else
                {
                    OnTimerTick();
                }
            }
        }

        public void MakeGuess(bool i_IsFirstPageHigher)
        {
            ProcessGuess(i_IsFirstPageHigher);
        }

        public void SelectNextPage(bool i_KeepFirstPage)
        {
            if(!IsGameOver)
            {
                MockPage nextPage = getNextPage();

                if(nextPage != null)
                {
                    if(!i_KeepFirstPage)
                    {
                        m_CurrentWinningPage = m_NewChallengingPage;
                    }

                    m_NewChallengingPage = nextPage;
                    ContinueToNextRound();
                } else
                {
                    IsGameOver = true;
                    OnGameOver(new GameOverEventArgs(Score));
                }
            }
        }

        public void StopTimer()
        {
            stopTimer();
        }

        public void HandleTimeExpired()
        {
            OnTimeExpired();
        }

        private static void shufflePages<T>(List<T> i_List)
        {
            int listCount = i_List.Count;

            for(int i = listCount - 1; i > 0; i--)
            {
                int randomPageIndex = sr_RandomPage.Next(0, i + 1);
                T temp = i_List[i];
                i_List[i] = i_List[randomPageIndex];
                i_List[randomPageIndex] = temp;
            }
        }

        private MockPage getNextPage()
        {
            MockPage nextPage = null;

            if(r_UnusedPages.Count > 0)
            {
                nextPage = r_UnusedPages[0];
                r_UnusedPages.RemoveAt(0);
                r_UsedPageIds.Add(nextPage.Id);
            }

            return nextPage;
        }

        protected virtual void OnPagesSelected(PageSelectedEventArgs e)
        {
            PagesSelected?.Invoke(this, e);
        }

        protected virtual void OnTimerTick()
        {
            bool isTimeRunningLow = m_RemainingSeconds <= r_GameConfiguration.LowTimeThreshold;
            TimerTick?.Invoke(this, new TimerEventArgs(m_RemainingSeconds, isTimeRunningLow));
        }

        protected virtual void OnTimeExpired()
        {
            TimeExpired?.Invoke(this, EventArgs.Empty);
        }

        private void initPagesList()
        {
            r_UnusedPages.Clear();
            r_UsedPageIds.Clear();

            if (r_LoggedInUser.LikedPages != null && r_LoggedInUser.LikedPages.Count > 0)
            {
                foreach (Page page in r_LoggedInUser.LikedPages)
                {
                    MockPage mockPage = new MockPage(page);

                    r_UnusedPages.Add(mockPage);
                }

                if (r_UnusedPages.Count < k_MinPagesRequired)
                {
                    throw new Exception("Not enough pages to start the game");
                }

                shufflePages(r_UnusedPages);
            }
            else
            {
                throw new Exception("Not enough pages to start the game");
            }
        }
    }
}