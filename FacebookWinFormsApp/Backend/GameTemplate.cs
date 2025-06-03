using System;

namespace FacebookDPApp.Backend
{
    public abstract class GameTemplate
    {
        protected bool m_IsRoundInProgress;
        public int Score { get; protected set; }
        public bool IsGameOver { get; protected set; }

        public event EventHandler<GameOverEventArgs> GameOver;

        public event EventHandler<GuessResultEventArgs> GuessResult;

        protected GameTemplate()
        {
            Score = 0;
            IsGameOver = false;
            m_IsRoundInProgress = false;
        }

        public void StartNewGame()
        {
            Score = 0;
            IsGameOver = false;
            m_IsRoundInProgress = false;

            InitializeGame();
            StartRound();
        }

        public void StartRound()
        {
            if (!m_IsRoundInProgress && !IsGameOver)
            {
                m_IsRoundInProgress = true;

                try
                {
                    if (PrepareRound())
                    {
                        DisplayRound();
                        StartTimer();
                    }
                    else
                    {
                        IsGameOver = true;
                        OnGameOver(new GameOverEventArgs(Score));
                    }
                }
                catch (Exception ex)
                {
                    HandleRoundError(ex);
                    m_IsRoundInProgress = false;
                }
            }
        }

        public void ContinueToNextRound()
        {
            if (!IsGameOver)
            {
                StartRound();
            }
        }

        public void ProcessGuess(bool i_UserGuess)
        {
            if (!IsGameOver && m_IsRoundInProgress)
            {
                stopTimer();

                GuessResultEventArgs resultArgs = EvaluateGuess(i_UserGuess);

                if (resultArgs.IsCorrect)
                {
                    UpdateScore();
                }

                OnGuessResult(resultArgs);
                m_IsRoundInProgress = false;
                CheckGameStatus();
            }
        }

        public void TimeExpired()
        {
            if (!IsGameOver && m_IsRoundInProgress)
            {
                ProcessGuess(GetDefaultGuess());
            }
        }

        protected abstract void InitializeGame();

        protected abstract bool PrepareRound();

        protected abstract void DisplayRound();
        protected abstract void StartTimer();
        protected abstract void stopTimer();

        protected abstract GuessResultEventArgs EvaluateGuess(bool i_UserGuess);
        protected abstract void UpdateScore();
        protected abstract void CheckGameStatus();

        protected abstract bool GetDefaultGuess();

        protected abstract void HandleRoundError(Exception i_Exception);

        protected virtual void OnGuessResult(GuessResultEventArgs i_EventArgs)
        {
            GuessResult?.Invoke(this, i_EventArgs);
        }

        protected virtual void OnGameOver(GameOverEventArgs i_EventArgs)
        {
            GameOver?.Invoke(this, i_EventArgs);
        }
    }
}
