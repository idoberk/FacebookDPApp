using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public delegate void PagesSelectedEventHandler(object sender, PageSelectedEventArgs e);

    public delegate void GuessResultEventHandler(object sender, GuessResultEventArgs e);

    public delegate void GameOverEventHandler(object sender, GameOverEventArgs e);

    public delegate void TimerTickEventHandler(object sender, TimerEventArgs e);

    public delegate void TimeExpiredEventHandler(object sender, EventArgs e);


    public class HigherLowerGameLogic
    {
        private const int k_TimeLimitSeconds = 15;
        private const int k_LowTimeThreshold = 5;
        private const int k_MinPagesRequired = 2;

        private readonly User r_LoggedInUser;
        private readonly List<MockPage> r_UnusedPages = new List<MockPage>();
        private readonly HashSet<string> r_UsedPageIds = new HashSet<string>();
        private MockPage m_CurrentWinningPage;
        private MockPage m_NewChallengingPage;
        private int m_Score;
        private bool m_IsGameOver;
        private static readonly Random sr_RandomPage = new Random();
        private int m_RemainingSeconds;
        private bool m_IsTimerRunning;

        public event PagesSelectedEventHandler PagesSelected;

        public event GuessResultEventHandler GuessResult;

        public event GameOverEventHandler GameOver;

        public event TimerTickEventHandler TimerTick;

        public event TimeExpiredEventHandler TimeExpired;

        public int Score
        {
            get { return m_Score; }
        }

        public bool IsGameOver
        {
            get { return m_IsGameOver; }
        }

        public int TimeLimit
        {
            get { return k_TimeLimitSeconds; }
        }

        public HigherLowerGameLogic(User i_LoggedInUser)
        {
            r_LoggedInUser = i_LoggedInUser;
            m_Score = 0;
            m_IsGameOver = false;
            m_RemainingSeconds = k_TimeLimitSeconds;
            m_IsTimerRunning = false;
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

        public void StartNewGame()
        {
            try
            {
                m_Score = 0;
                m_IsGameOver = false;
                m_RemainingSeconds = k_TimeLimitSeconds;
                m_IsTimerRunning = false;

                initPagesList();

                m_CurrentWinningPage = getNextPage();
                m_NewChallengingPage = getNextPage();

                if (m_CurrentWinningPage != null && m_NewChallengingPage != null)
                {
                    OnPagesSelected(
                        new PageSelectedEventArgs(
                            m_CurrentWinningPage.GetOriginalPage(),
                            m_NewChallengingPage.GetOriginalPage()));

                    startTimer();
                }
                else
                {
                    throw new Exception("Not enough pages to start the game");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error starting the game: " + ex.Message);
            }
        }

        private void startTimer()
        {
            if (!m_IsGameOver)
            {
                m_RemainingSeconds = k_TimeLimitSeconds;
                m_IsTimerRunning = true;
                OnTimerTick();
            }
        }

        public void StopTimer()
        {
            m_IsTimerRunning = false;
        }

        public void TimerTicks()
        {
            if (m_IsTimerRunning && !m_IsGameOver)
            {
                m_RemainingSeconds--;

                if (m_RemainingSeconds <= 0)
                {
                    m_RemainingSeconds = 0;
                    m_IsTimerRunning = false;
                    OnTimeExpired();
                }
                else
                {
                    OnTimerTick();
                }
            }
        }

        private static void shufflePages<T>(List<T> i_List)
        {
            int listCount = i_List.Count;

            for (int i = listCount - 1; i > 0; i--)
            {
                int randomPageIndex = sr_RandomPage.Next(0, i + 1);

                T temp = i_List[i];
                i_List[i] = i_List[randomPageIndex];
                i_List[randomPageIndex] = temp;
            }
        }

        public void MakeGuess(bool i_IsFirstPageHigher)
        {
            if (m_IsGameOver)
            {
                return;
            }

            StopTimer();

            bool isFirstPageHigher = m_CurrentWinningPage.LikesCount > m_NewChallengingPage.LikesCount;
            bool isCorrectGuess = (i_IsFirstPageHigher == isFirstPageHigher);

            if (isCorrectGuess)
            {
                m_Score++;
            }

            OnGuessResult(
                new GuessResultEventArgs(
                    (int)m_CurrentWinningPage.LikesCount,
                    (int)m_NewChallengingPage.LikesCount,
                    isCorrectGuess,
                    isFirstPageHigher));
        }

        public void SelectNextPage(bool i_KeepFirstPage)
        {
            if (m_IsGameOver)
            {
                return;
            }

            selectNextPage(i_KeepFirstPage);
        }

        private void selectNextPage(bool i_KeepFirstPage)
        {
            MockPage nextPage = getNextPage();

            if (nextPage != null)
            {
                if (!i_KeepFirstPage)
                {
                    m_CurrentWinningPage = m_NewChallengingPage;
                }

                m_NewChallengingPage = nextPage;
                OnPagesSelected(
                    new PageSelectedEventArgs(
                        m_CurrentWinningPage.GetOriginalPage(),
                        m_NewChallengingPage.GetOriginalPage()));

                startTimer();
            }
            else
            {
                m_IsGameOver = true;
                OnGameOver(new GameOverEventArgs(m_Score));
            }
        }

        private MockPage getNextPage()
        {
            MockPage nextPage = null;

            if (r_UnusedPages.Count > 0)
            {
                nextPage = r_UnusedPages[0];
                r_UnusedPages.RemoveAt(0);
                r_UsedPageIds.Add(nextPage.Id);
            }

            return nextPage;
        }

        public void HandleTimeExpired()
        {
            if (!m_IsGameOver)
            {
                MakeGuess(i_IsFirstPageHigher: true);
            }
        }

        protected virtual void OnTimerTick()
        {
            bool isTimeRunningLow = m_RemainingSeconds <= k_LowTimeThreshold;

            TimerTick?.Invoke(this, new TimerEventArgs(m_RemainingSeconds, isTimeRunningLow));
        }

        protected virtual void OnTimeExpired()
        {
            TimeExpired?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPagesSelected(PageSelectedEventArgs e)
        {
            PagesSelected?.Invoke(this, e);
        }

        protected virtual void OnGuessResult(GuessResultEventArgs e)
        {
            GuessResult?.Invoke(this, e);
        }

        protected virtual void OnGameOver(GameOverEventArgs e)
        {
            GameOver?.Invoke(this, e);
        }
    }
}