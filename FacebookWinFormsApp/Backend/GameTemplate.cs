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

            initializeGame();
            StartRound();
        }

        public void StartRound()
        {
            if (!m_IsRoundInProgress && !IsGameOver)
            {
                m_IsRoundInProgress = true;

                try
                {
                    if (prepareRound())
                    {
                        displayRound();
                        startTimer();
                    }
                    else
                    {
                        IsGameOver = true;
                        OnGameOver(new GameOverEventArgs(Score));
                    }
                }
                catch (Exception ex)
                {
                    handleRoundError(ex);
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

                GuessResultEventArgs resultArgs = evaluateGuess(i_UserGuess);

                if (resultArgs.IsCorrect)
                {
                    updateScore();
                }

                OnGuessResult(resultArgs);
                m_IsRoundInProgress = false;
                checkGameStatus();
            }
        }

        public void TimeExpired()
        {
            if (!IsGameOver && m_IsRoundInProgress)
            {
                ProcessGuess(getDefaultGuess());
            }
        }

        protected abstract void initializeGame();

        protected abstract bool prepareRound();

        protected abstract void displayRound();
        protected abstract void startTimer();
        protected abstract void stopTimer();

        protected abstract GuessResultEventArgs evaluateGuess(bool i_UserGuess);
        protected abstract void updateScore();
        protected abstract void checkGameStatus();

        protected abstract bool getDefaultGuess();

        protected abstract void handleRoundError(Exception i_Exception);

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
