using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookDPApp.CustomControls;
using FacebookWrapper.ObjectModel;

namespace FacebookDPApp.Backend
{
    public class HigherLowerGameManager
    {
        private readonly Label r_LabelScore;
        private readonly Label r_LabelTimer;
        private readonly Label r_LabelRoundFeedback;
        private readonly Label r_LabelPage1Name;
        private readonly Label r_LabelPage2Name;
        private readonly Label r_LabelPage1Likes;
        private readonly Label r_LabelPage2Likes;

        private readonly PictureBox r_PictureBoxPage1;
        private readonly PictureBox r_PictureBoxPage2;

        private readonly RoundedButton r_ButtonHigherPage1;
        private readonly RoundedButton r_ButtonHigherPage2;
        private readonly RoundedButton r_ButtonNewGame;

        private readonly Control r_ParentControl;

        private HigherLowerGameLogic m_GameLogic;
        private readonly User r_LoggedInUser;
        private Timer m_UiTimer;
        private Timer m_DelayTimer;
        private bool m_IsGameInit;
        private const int k_DelayBetweenRoundsMs = 3000;

        public HigherLowerGameManager(
            Label i_LabelScore,
            Label i_LabelTimer,
            Label i_LabelRoundFeedback,
            Label i_LabelPage1Name,
            Label i_LabelPage2Name,
            Label i_LabelPage1Likes,
            Label i_LabelPage2Likes,
            PictureBox i_PictureBoxPage1,
            PictureBox i_PictureBoxPage2,
            RoundedButton i_ButtonHigherPage1,
            RoundedButton i_ButtonHigherPage2,
            RoundedButton i_ButtonNewGame,
            Control i_ParentControl,
            User i_LoggedInUser)
        {
            r_LabelScore = i_LabelScore;
            r_LabelTimer = i_LabelTimer;
            r_LabelRoundFeedback = i_LabelRoundFeedback;
            r_LabelPage1Name = i_LabelPage1Name;
            r_LabelPage2Name = i_LabelPage2Name;
            r_LabelPage1Likes = i_LabelPage1Likes;
            r_LabelPage2Likes = i_LabelPage2Likes;
            r_PictureBoxPage1 = i_PictureBoxPage1;
            r_PictureBoxPage2 = i_PictureBoxPage2;
            r_ButtonHigherPage1 = i_ButtonHigherPage1;
            r_ButtonHigherPage2 = i_ButtonHigherPage2;
            r_ButtonNewGame = i_ButtonNewGame;
            r_ParentControl = i_ParentControl;
            r_LoggedInUser = i_LoggedInUser;
            m_IsGameInit = false;
        }

        public void Initialize()
        {
            if (!m_IsGameInit)
            {
                initTimer();
                initHigherLowerGame();
                m_IsGameInit = true;
            }
            else
            {
                initStateUi();
            }
        }

        private void initHigherLowerGame()
        {
            try
            {
                if (m_GameLogic == null)
                {
                    m_GameLogic = new HigherLowerGameLogic(r_LoggedInUser);
                }

                attachEventHandlers();
                attachButtonsEventHandlers();
                initStateUi();
            }
            catch (Exception ex)
            {
                handleGameInitError(ex);
            }
        }

        private void startNewGame()
        {
            if (m_GameLogic != null)
            {
                Cleanup();
                initTimer();
                attachEventHandlers();
                attachButtonsEventHandlers();
                resetUiForNewGame();
                r_ButtonNewGame.Visible = false;
                m_UiTimer.Start();
                m_GameLogic.StartNewGame();
            }
        }

        public void Cleanup()
        {
            m_GameLogic.StopTimer();
            detachEventHandlers();
            stopAndDisposeTimers();
            detachButtonsEventHandlers();
            r_ButtonNewGame.Visible = true;
            m_IsGameInit = false;
        }

        private void detachEventHandlers()
        {
            m_GameLogic.PagesSelected -= GameLogic_PagesSelected;
            m_GameLogic.GuessResult -= GameLogic_GuessResult;
            m_GameLogic.GameOver -= GameLogic_GameOver;
            m_GameLogic.TimerTick -= GameLogic_TimerTick;
            m_GameLogic.TimeExpired -= GameLogic_TimeExpired;
        }

        private void attachEventHandlers()
        {
            if (m_GameLogic != null)
            {
                m_GameLogic.PagesSelected += GameLogic_PagesSelected;
                m_GameLogic.GuessResult += GameLogic_GuessResult;
                m_GameLogic.GameOver += GameLogic_GameOver;
                m_GameLogic.TimerTick += GameLogic_TimerTick;
                m_GameLogic.TimeExpired += GameLogic_TimeExpired;
            }
        }

        private void stopAndDisposeTimers()
        {
            if (m_UiTimer != null)
            {
                m_UiTimer.Stop();
                m_UiTimer.Dispose();
                m_UiTimer = null;
            }
        }

        private void initTimer()
        {
            m_UiTimer = new Timer();

            m_UiTimer.Interval = 1000;
            m_UiTimer.Tick += UiTimer_Tick;
        }

        private void detachButtonsEventHandlers()
        {
            r_ButtonHigherPage1.Click -= buttonHigherPage1_Click;
            r_ButtonHigherPage2.Click -= buttonHigherPage2_Click;
            r_ButtonNewGame.Click -= buttonNewGame_Click;
        }

        private void attachButtonsEventHandlers()
        {
            r_ButtonHigherPage1.Click += buttonHigherPage1_Click;
            r_ButtonHigherPage2.Click += buttonHigherPage2_Click;
            r_ButtonNewGame.Click += buttonNewGame_Click;
        }

        private void resetUiForNewGame()
        {
            r_LabelScore.Text = "Score: 0";

            r_LabelTimer.Text = $"Time: {m_GameLogic.TimeLimit}s";
            r_LabelTimer.ForeColor = Color.Blue;

            r_ButtonHigherPage1.Enabled = true;
            r_ButtonHigherPage2.Enabled = true;

            r_LabelRoundFeedback.Visible = false;
        }

        private void initStateUi()
        {
            r_LabelScore.Text = "Score: 0";

            r_LabelTimer.Text = $"Time: {m_GameLogic.TimeLimit}s";
            r_LabelTimer.ForeColor = Color.Blue;

            r_LabelPage1Name.Text = "Page Name";
            r_LabelPage2Name.Text = "Page Name";

            r_LabelRoundFeedback.Visible = false;

            r_LabelPage1Likes.Visible = false;
            r_LabelPage2Likes.Visible = false;

            r_ButtonHigherPage1.Enabled = false;
            r_ButtonHigherPage2.Enabled = false;

            r_PictureBoxPage1.ImageLocation = null;
            r_PictureBoxPage2.ImageLocation = null;

            r_ButtonNewGame.Visible = true;
        }

        private void handleGameInitError(Exception i_Exception)
        {
            MessageBox.Show(
                $"Error starting the game: {i_Exception.Message} Make sure you have liked at least 2 Facebook pages",
                "Game Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            r_ButtonHigherPage1.Enabled = false;
            r_ButtonHigherPage2.Enabled = false;
        }

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            m_GameLogic?.TimerTicks();
        }

        private void GameLogic_PagesSelected(object sender, PageSelectedEventArgs e)
        {
            r_ParentControl.Invoke(new Action(() => updatePagesDisplay(e)));
        }

        private void GameLogic_GuessResult(object sender, GuessResultEventArgs e)
        {
            r_ParentControl.Invoke(new Action(() => processGuessResult(e)));
        }

        private void GameLogic_GameOver(object sender, GameOverEventArgs e)
        {
            r_ParentControl.Invoke(new Action(() => handleGameOver(e)));
        }

        private void GameLogic_TimerTick(object sender, TimerEventArgs e)
        {
            r_ParentControl.Invoke(new Action(() => updateTimerDisplay(e)));
        }

        private void GameLogic_TimeExpired(object sender, EventArgs e)
        {
            r_ParentControl.Invoke(new Action(() => handleTimeExpired()));
        }

        private void updatePagesDisplay(PageSelectedEventArgs i_PageData)
        {
            try
            {
                displayPageInfo(i_PageData);
                hidePageLikes();
                enableGuessButtons();
            }
            catch (Exception ex)
            {
                showPageDisplayError(ex);
            }
        }

        private void displayPageInfo(PageSelectedEventArgs i_PageData)
        {
            r_PictureBoxPage1.ImageLocation = i_PageData.FirstPage.PictureURL;
            r_PictureBoxPage2.ImageLocation = i_PageData.SecondPage.PictureURL;

            r_LabelPage1Name.Text = i_PageData.FirstPage.Name;
            r_LabelPage2Name.Text = i_PageData.SecondPage.Name;
        }

        private void hidePageLikes()
        {
            r_LabelPage1Likes.Visible = false;
            r_LabelPage2Likes.Visible = false;
        }

        private void enableGuessButtons()
        {
            r_ButtonHigherPage1.Enabled = true;
            r_ButtonHigherPage2.Enabled = true;
        }

        private static void showPageDisplayError(Exception i_Exception)
        {
            MessageBox.Show(
                $"Error displaying pages: {i_Exception.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void processGuessResult(GuessResultEventArgs i_GuessResult)
        {
            try
            {
                displayGuessResult(i_GuessResult);
                scheduleNextRound(i_GuessResult.IsFirstPageHigher);
            }
            catch (Exception ex)
            {
                showGuessResultError(ex);
            }
        }

        private void updatePageLikesDisplay(GuessResultEventArgs i_GuessResult)
        {
            r_LabelPage1Likes.Visible = true;
            r_LabelPage2Likes.Visible = true;

            r_LabelPage1Likes.Text = $"{i_GuessResult.FirstPageLikesCount:N0} likes";
            r_LabelPage2Likes.Text = $"{i_GuessResult.SecondPageLikesCount:N0} likes";
        }

        private void updateScoreDisplay()
        {
            r_LabelScore.Text = $"Score: {m_GameLogic.Score}";
        }

        private void disableGuessButtons()
        {
            r_ButtonHigherPage1.Enabled = false;
            r_ButtonHigherPage2.Enabled = false;
        }

        private void cleanupExistingDelayTimer()
        {
            if (m_DelayTimer != null)
            {
                m_DelayTimer.Stop();
                m_DelayTimer.Dispose();
                m_DelayTimer = null;
            }
        }

        private void displayGuessResult(GuessResultEventArgs i_GuessResult)
        {
            updatePageLikesDisplay(i_GuessResult);
            updateScoreDisplay();
            disableGuessButtons();
            showFeedbackLabel(i_GuessResult.IsCorrect);
        }

        private void showFeedbackLabel(bool i_IsCorrect)
        {
            if (i_IsCorrect)
            {
                r_LabelRoundFeedback.Text = "Correct!";
                r_LabelRoundFeedback.BackColor = Color.Green;
            }
            else
            {
                r_LabelRoundFeedback.Text = "Wrong!";
                r_LabelRoundFeedback.BackColor = Color.Red;
            }

            r_LabelRoundFeedback.Visible = true;
        }

        private static void showGuessResultError(Exception i_Exception)
        {
            MessageBox.Show(
                $"Error processing guess result: {i_Exception.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void scheduleNextRound(bool i_IsFirstPageHigher)
        {
            cleanupExistingDelayTimer();

            m_DelayTimer = new Timer();

            m_DelayTimer.Interval = k_DelayBetweenRoundsMs;
            m_DelayTimer.Tick += (s, args) =>
                {
                    r_LabelRoundFeedback.Visible = false;

                    cleanupExistingDelayTimer();

                    m_GameLogic.SelectNextPage(i_IsFirstPageHigher);
                };
            m_DelayTimer.Start();
        }

        private void handleGameOver(GameOverEventArgs i_GameOverData)
        {
            try
            {
                disableGuessButtons();
                r_ButtonNewGame.Visible = true;
                showGameOverMessage(i_GameOverData.FinalScore);
            }
            catch (Exception ex)
            {
                showGameOverError(ex);
            }
        }

        private static void showGameOverMessage(int i_FinalScore)
        {
            MessageBox.Show(
                $"Game Over! Your final score is: {i_FinalScore}",
                "Game Over",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void showGameOverError(Exception i_Exception)
        {
            MessageBox.Show(
                $"Error handling game over: {i_Exception.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void updateTimerDisplay(TimerEventArgs i_TimerData)
        {
            r_LabelTimer.Text = $"Time: {i_TimerData.RemainingSeconds}s";
            r_LabelTimer.ForeColor = i_TimerData.IsTimeRunningOut ? Color.Red : Color.Blue;
        }

        private void handleTimeExpired()
        {
            r_LabelRoundFeedback.Text = "TIME OVER!";
            r_LabelRoundFeedback.BackColor = Color.Red;
            r_LabelRoundFeedback.Visible = true;

            if (m_GameLogic.IsGameOver)
            {
                r_ButtonNewGame.Visible = true;
            }

            r_LabelRoundFeedback.Visible = true;
            startTimeExpiredTimer();
        }

        private void startTimeExpiredTimer()
        {
            Timer timeUpTimer = new Timer();

            timeUpTimer.Interval = 1000;
            timeUpTimer.Tick += TimeUpTimer_Tick;
            timeUpTimer.Start();
        }

        private void TimeUpTimer_Tick(object sender, EventArgs e)
        {
            Timer timer = sender as Timer;

            timer.Stop();
            timer.Dispose();

            r_LabelRoundFeedback.Visible = false;

            if (!m_GameLogic.IsGameOver)
            {
                m_GameLogic.HandleTimeExpired();
            }
        }

        private void buttonHigherPage1_Click(object sender, EventArgs e)
        {
            m_GameLogic?.MakeGuess(true);
        }

        private void buttonHigherPage2_Click(object sender, EventArgs e)
        {
            m_GameLogic?.MakeGuess(false);
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            startNewGame();
        }
    }
}