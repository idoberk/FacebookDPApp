namespace FacebookDPApp.Backend
{
    public class GameConfiguration
    {
        public int InitialTimeSeconds { get; private set; }

        public int MinTimeSeconds { get; private set; }

        public int TimeDecreaseAmount { get; private set; }

        public int PointsPerCorrectAnswer { get; private set; }

        public int TimeBonusMultiplier { get; private set; }

        public int MaxWrongAnswers { get; private set; }

        public int LowTimeThreshold { get; private set; }

        public bool DecreaseTimeEachRound { get; private set; }

        public bool EnableTimeBonus { get; private set; }

        public bool ResetTimeOnWrongAnswer { get; private set; }

        public string ModeName { get; private set; }

        public static GameConfiguration CreateGameConfiguration(eGameMode i_GameMode)
        {
            switch (i_GameMode)
            {
                case eGameMode.Easy:
                    return createEasyConfiguration();
                case eGameMode.Normal:
                    return createNormalConfiguration();
                case eGameMode.Hard:
                    return createHardConfiguration();

                default:
                    return createEasyConfiguration();
            }
        }

        private static GameConfiguration createEasyConfiguration()
        {
            return new GameConfiguration
                       {
                           ModeName = "Easy",
                           InitialTimeSeconds = 30,
                           MinTimeSeconds = 30,
                           DecreaseTimeEachRound = false,
                           TimeDecreaseAmount = 0,
                           PointsPerCorrectAnswer = 50,
                           EnableTimeBonus = false,
                           TimeBonusMultiplier = 0,
                           MaxWrongAnswers = -1,
                           ResetTimeOnWrongAnswer = false,
                           LowTimeThreshold = 5
                       };
        }

        private static GameConfiguration createNormalConfiguration()
        {
            return new GameConfiguration
                       {
                           ModeName = "Normal",
                           InitialTimeSeconds = 15,
                           MinTimeSeconds = 5,
                           DecreaseTimeEachRound = true,
                           TimeDecreaseAmount = 1,
                           PointsPerCorrectAnswer = 100,
                           EnableTimeBonus = true,
                           TimeBonusMultiplier = 3,
                           MaxWrongAnswers = -1,
                           ResetTimeOnWrongAnswer = true,
                           LowTimeThreshold = 5
                       };
        }

        private static GameConfiguration createHardConfiguration()
        {
            return new GameConfiguration
                       {
                           ModeName = "Hard",
                           InitialTimeSeconds = 8,
                           MinTimeSeconds = 3,
                           DecreaseTimeEachRound = true,
                           TimeDecreaseAmount = 2,
                           PointsPerCorrectAnswer = 200,
                           EnableTimeBonus = true,
                           TimeBonusMultiplier = 5,
                           MaxWrongAnswers = 1,
                           ResetTimeOnWrongAnswer = true,
                           LowTimeThreshold = 3
                       };
        }
    }
}