using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hi5_Interaction_Core
{
	public class Hi5_Glove_Gesture_Recognition
	{
		Hi5_Glove_Interaction_Hand mHand = null;
		Hi5_Glove_Gesture_Recognition_Record mRecord = null;
		internal Hi5_Glove_Gesture_Recognition (Hi5_Glove_Interaction_Hand hand)
		{
			mRecord = new Hi5_Glove_Gesture_Recognition_Record ();
			mHand = hand;
		}
        internal bool IsWantPinch = false;
		internal void Update(float detTime)
        {

            /* MATR */ 
            //palmOrientation(); 

            /* EMATR */ 
            if (IsCloseThumbAndIndexCollider())
            {
                mHand.mVisibleHand.SetThumbAndIndexFingerCollider(false);
            }
            else
                mHand.mVisibleHand.SetThumbAndIndexFingerCollider(true);

            if (IsHandFist()) // cerrar la mano en forma de puño.
            {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.EFist);
                mState = Hi5_Glove_Gesture_Recognition_State.EFist;
                //mHand.mVisibleHand.ChangeColor(Color.yellow);
            }
            /*else if (IsHandIndexPoint()) // dedo indice estirado
            {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.EIndexPoint);
                mState = Hi5_Glove_Gesture_Recognition_State.EIndexPoint;
                mHand.mVisibleHand.ChangeColor(Color.blue);
            }*/

            else if (IsHandPlane()) // palma de la mano
            {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.EHandPlane);
                mState = Hi5_Glove_Gesture_Recognition_State.EHandPlane;
                //mHand.mVisibleHand.ChangeColor(Color.green);
            }
            else if (IsOk())
            {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.EOk);
                mState = Hi5_Glove_Gesture_Recognition_State.EOk;
                //mHand.mVisibleHand.ChangeColor(Color.yellow);
            }
        
            /* MATR */ 
            else if(isIndexFingerUp()) {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.EIndexUp); 
                mState = Hi5_Glove_Gesture_Recognition_State.EIndexUp; 
                //mHand.mVisibleHand.ChangeColor(Color.white); 
            }
            
            
            else
            {
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.ENone);
                mState = Hi5_Glove_Gesture_Recognition_State.ENone;
                //mHand.mVisibleHand.ChangeColor(mHand.mVisibleHand.orgColor);
            }
            //if (Hi5_Interaction_Const.TestPinchOpenCollider)
            //{
            //    if (IsFlyPinch() || IsPinch2())
            //    {
            //        mHand.mVisibleHand.ChangeColor(Color.blue);
            //        IsWantPinch = true;
            //    }
            //    else
            //    {
            //        mHand.mVisibleHand.ChangeColor(mHand.mVisibleHand.orgColor);
            //        IsWantPinch = false;
            //    }
            //}
              
        }

        /* MATR */ 

        internal void palmOrientation() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                float angle = mHand.mVisibleHand.palmOrientation(); 
                if(angle > 0.0f && angle < 30.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.black); 
                    //Debug.Log("negro"); 
                }
                else if(angle > 30.0f && angle < 60.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.blue); 
                    //Debug.Log("azul"); 
                }
                else if(angle > 60.0f && angle < 90.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.cyan); 
                    //Debug.Log("cian"); 
                }
                else if(angle > 90.0f && angle < 120.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.gray); 
                    //Debug.Log("gris"); 
                }
                else if(angle > 120.0f && angle < 150.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.green); 
                    //Debug.Log("verde"); 
                }
                else if(angle > 150.0f && angle < 180.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.grey); 
                    //Debug.Log("gris2"); 
                }
                else if(angle > 180.0f && angle < 210.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.magenta); 
                    //Debug.Log("magenta"); 
                }
                else if(angle > 210.0f && angle < 240.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.red); 
                    //Debug.Log("rojo"); 
                }
                else if(angle > 240.0f && angle < 270.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.white); 
                    //Debug.Log("blanco"); 
                }
                else if(angle > 270.0f && angle < 300.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.yellow); 
                    //Debug.Log("amarillo"); 
                }
                else if(angle > 300.0f && angle < 360.0f) {
                    mHand.mVisibleHand.ChangeColor(Color.clear); 
                    //Debug.Log("transparente"); 
                }

            }
        }

        internal bool isIndexFingerUp() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                return mHand.mState.mJudgeMent.isIndexFingerUp(); 
            }
            else{
                return false; 
            }
        }

        /* EMATR */ 



        internal bool IsOk()
        {
           // return mHand.mFingers[Hi5_Glove_Interaction_Finger_Type.EThumb].IsTumbColliderIndex();
            if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null)
            {
                return mHand.mState.mJudgeMent.IsOK();
            }
            else
                return false;
        }

        internal bool IsCloseThumbAndIndexCollider()
        {
            if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null)
            {
                return mHand.mState.mJudgeMent.IsCloseThumbAndIndexCollider();
            }
            else
                return false;
        }

        internal bool IsFlyPinch()
		{
			if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
				return mHand.mState.mJudgeMent.IsGestureFlyPinch ();
			} else
				return false;
		}

        internal bool IsPinch2()
        {
            if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null)
            {
                return mHand.mState.mJudgeMent.IsGesturePinch2();
            }
            else
                return false;
        }

		internal bool IsHandPlane()
		{
			if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
				return mHand.mState.mJudgeMent.IsFingerPlane ();
			} else
				return false;
		}

		internal bool IsHandFist()
		{
			if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
				return mHand.mState.mJudgeMent.IsHandFist ();
			} else
				return false;
		}

        internal bool IsHandIndexPoint()
        {
            if (mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null)
            {
                return mHand.mState.mJudgeMent.IsHandIndexPoint();
            }
            else
                return false;
        }
        
        internal bool IsRecordFlyPinch()
		{
			return mRecord.IsHaveGesture (Hi5_Glove_Gesture_Recognition_State.EOk);
		}

		internal void CleanRecord()
		{
			mRecord.RecordClean ();
		}

        internal Hi5_Glove_Gesture_Recognition_State mState = Hi5_Glove_Gesture_Recognition_State.ENone;
        internal Hi5_Glove_Gesture_Recognition_State GetRecognitionState()
        {
            return mState;
        }
    }
	public enum Hi5_Glove_Gesture_Recognition_State
	{
		ENone = 0,
		EOk,
		EFist,
        EIndexPoint,
		EHandPlane, 
        /* MATR */
        EIndexUp
	}

	public class Hi5_Glove_Gesture_Recognition_Record
	{
		internal Hi5_Glove_Gesture_Recognition_State mState = Hi5_Glove_Gesture_Recognition_State.ENone;
		Queue<Hi5_Glove_Gesture_Recognition_State> mQueuePositionRecord = new Queue<Hi5_Glove_Gesture_Recognition_State>();
		internal void RecordClean()
		{
			mQueuePositionRecord.Clear();
		}

		internal Queue<Hi5_Glove_Gesture_Recognition_State> GetRecord()
		{
			return mQueuePositionRecord;
		}

		internal void RecordGesture(Hi5_Glove_Gesture_Recognition_State state)
		{
			mState = state;
			if (mQueuePositionRecord.Count > (5 - 1))
			{
				mQueuePositionRecord.Dequeue();
			}
			mQueuePositionRecord.Enqueue(state);
		}

		internal bool IsHaveGesture(Hi5_Glove_Gesture_Recognition_State state)
		{
			foreach (Hi5_Glove_Gesture_Recognition_State item in mQueuePositionRecord) 
			{
				if (item == state)
					return true;
			}
			return false;
		}
	}
}