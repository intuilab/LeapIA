using IntuiLab.Leap.Events;
using IntuiLab.Leap.Utils;
using Leap;

namespace IntuiLab.Leap
{
    public partial class LeapListener
    {
        #region Fields
        
        /// <summary>
        /// For the events HandPresent/HandLost
        /// </summary>
        private bool m_HandInPreviousFrame;

        #endregion

        #region Events

        /************ Events raised when hands are present *******************/

        /// <summary>
        /// Event raised when a hand is detected
        /// </summary>
        public event HandEventHandler HandPresent;

        /// <summary>
        /// Event raised when a hand disappears
        /// </summary>
        public event HandEventHandler HandRemoved;


        private void RaiseHandPresentEvent()
        {
            // if there is a subscriber to the event
            if (HandPresent != null)
                HandPresent(this, new HandEventArgs());
        }

        private void RaiseHandRemovedEvent()
        {
            if (HandRemoved != null)
                HandRemoved(this, new HandEventArgs());
        }

        /***********************************************************************/

        #endregion

        private void HandDetection(HandList hands, FingerList fingers)
        {
            /********************** HAND DETECTION ************************************/
            if ((!hands.IsEmpty && LeapMath.IsAboveTheLeap(hands)) || (!fingers.IsEmpty && LeapMath.IsAboveTheLeap(fingers))) // if there is at least one hand or one finger, and if it is more or less above the device
            {
                // and if this hand was not present in the previous frame
                if (!m_HandInPreviousFrame)
                {
                    RaiseHandPresentEvent(); // we raise the HandPresent event
                    m_HandInPreviousFrame = true;
                }
            }
            else
            {
                if (m_HandInPreviousFrame)
                {
                    RaiseHandRemovedEvent(); // but if there is no hand or finger in this frame, but there was in the previous frame, we raise the HandRemoved event
                    m_HandInPreviousFrame = false;
                }
            }
            /***************************************************************************/
        }
    }
}
