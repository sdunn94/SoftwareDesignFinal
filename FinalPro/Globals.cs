using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace FinalPro
{
    public static class Globals
    {
        static Analyze analysis = new Analyze();

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static Analyze GlobalAnalysis
        {
            get
            {
                return analysis;
            }
            set
            {
                analysis = value;
            }
        }

        //List<int> deadCounters = new List<int>();

        static int deadCounterDuke = 0;
        static int deadCounterAssassin = 0;
        static int deadCounterContessa = 0;
        static int deadCounterCaptain = 0;
        static int deadCounterAmbassador = 0;

        public static int DukeCounter
        {
            get
            {
                return deadCounterDuke;
            }
            set
            {
                deadCounterDuke = value;
            }
        }
        public static int AssassinCounter
        {
            get
            {
                return deadCounterAssassin;
            }
            set
            {
                deadCounterAssassin = value;
            }
        }
        public static int ContessaCounter
        {
            get
            {
                return deadCounterContessa;
            }
            set
            {
                deadCounterContessa = value;
            }
        }
        public static int CaptainCounter
        {
            get
            {
                return deadCounterCaptain;
            }
            set
            {
                deadCounterCaptain = value;
            }
        }
        public static int AmbassadorCounter
        {
            get
            {
                return deadCounterAmbassador;
            }
            set
            {
                deadCounterAmbassador = value;
            }
        }

        static string challengedOrBlockedPlayer;

        public static string CorBPlayer
        {
            get
            {
                return challengedOrBlockedPlayer;
            }
            set
            {
                challengedOrBlockedPlayer = value;
            }
        }

        static string playerBeingAssassinated;

        public static string AssassinatedPlayer
        {
            get
            {
                return playerBeingAssassinated;
            }
            set
            {
                playerBeingAssassinated = value;
            }
        }

        static string playerAssassinating;

        public static string PlayerAssassinating
        {
            get
            {
                return playerAssassinating;
            }
            set
            {
                playerAssassinating = value;
            }
        }

        static string playerBlockingAssassination;

        public static string PlayerBlockingAssassination
        {
            get
            {
                return playerBlockingAssassination;
            }
            set
            {
                playerBlockingAssassination = value;
            }
        }

        static string challengeStatus;

        public static string ChallengeStatus
        {
            get
            {
                return challengeStatus;
            }
            set
            {
                challengeStatus = value;
            }
        }

        static float stats;

        public static float Stats
        {
            get
            {
                return stats;
            }
            set
            {
                stats = value;
            }
        }
    }
}