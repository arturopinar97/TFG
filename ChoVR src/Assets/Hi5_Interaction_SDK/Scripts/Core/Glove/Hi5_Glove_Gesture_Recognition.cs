using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Modified for ChoVR */  
using ChoVR_Core; 
/* End block modifications for ChoVR */  
namespace Hi5_Interaction_Core
{
	public class Hi5_Glove_Gesture_Recognition
	{
		Hi5_Glove_Interaction_Hand mHand = null;
		Hi5_Glove_Gesture_Recognition_Record mRecord = null;
        /* Modified for ChoVR */ 
        AdvancedController advancedController; 
        static bool isLocked; 
        /* End Modified for ChoVR */ 
		internal Hi5_Glove_Gesture_Recognition (Hi5_Glove_Interaction_Hand hand)
		{
			mRecord = new Hi5_Glove_Gesture_Recognition_Record ();
			mHand = hand;
            /* Modified for ChoVR */ 
            isLocked = false; 
            /* End mosified for ChoVR */ 

		}
        internal bool IsWantPinch = false;
        /* Modified for ChoVR */ 
    
		internal void Update()
        {
            if(!isLocked) {
            if(isExtendedPalmUp()){
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.EIncreaseVolume); 
                handlerExtendedPalmGesture(); 
                //Debug.Log("Gesto palma arriba"); 
                //Debug.Log("Target: " + AdvancedController.getTarget()); 
                //mHand.mVisibleHand.ChangeColor(Color.blue); 
                AdvancedController.handlerGesture(); 
            }

            else if(isExtendedPalmDown()) {
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.ELowerVolume); 
                //Debug.Log("Palma hacia abajo"); 
                //mHand.mVisibleHand.ChangeColor(Color.yellow); 
                handlerExtendedPalmGesture(); 
                //Debug.Log("Gesto palma abajo");
                //Debug.Log("Target: " + AdvancedController.getTarget()); 
                AdvancedController.handlerGesture();
            }

            else if(IsHandFist()) {
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.EMute); 
                handlerFistGesture(); 
                //mHand.mVisibleHand.ChangeColor(Color.red); 
                AdvancedController.handlerGesture();
            }
            else if(isGesturePointUp()) {
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.EIncreaseTone);
                //Debug.Log("Detecto apuntar hacia arriba"); 
                //mHand.mVisibleHand.ChangeColor(Color.green); 
                //Debug.Log("Gesto de señalar hacia arriba"); 
                handlerPointGestureUpDown(); 
                AdvancedController.handlerGesture();
            }

            else if(isGesturePointDown()) {
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.ELowerTone);
                //Debug.Log("Detecto apuntar hacia abajo"); 
                //mHand.mVisibleHand.ChangeColor(Color.white); 
                //Debug.Log("Gesto de señalar hacia abajo"); 
                handlerPointGestureUpDown(); 
                AdvancedController.handlerGesture();
            }

            else if(isGesturePointAhead()) {
                isLocked = true; 
                AdvancedController.setGesture(AdvancedController.EGesture.ESign);
                //Debug.Log("Detecto apuntar a alguien"); 
                //mHand.mVisibleHand.ChangeColor(Color.black);
                //Debug.Log("Gesto de señalar personaje");  
                handlerPointGesture();
                AdvancedController.handlerGesture();
            }
            else{
                isLocked = true; 
                mRecord.RecordGesture(Hi5_Glove_Gesture_Recognition_State.ENone);
                mState = Hi5_Glove_Gesture_Recognition_State.ENone;
                mHand.mVisibleHand.ChangeColor(mHand.mVisibleHand.orgColor);
                
            }
            isLocked = false; 
            }
        }


        internal void handlerPointGesture() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                List<string> collisionTargets = AdvancedController.getCollisionTargets(); 
                string currentTarget = mHand.mVisibleHand.searchCollisionRaycastObjectFromPointFinger(collisionTargets); 
                AdvancedController.selectTarget(currentTarget); 
                AdvancedController.setCurrentHand(mHand.mVisibleHand.getCurrentHand());

                //Debug.Log("Current target: " + currentTarget); 
            }
        }
        internal void handlerPointGestureUpDown() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                List<string> collisionTargets = AdvancedController.getCollisionTargets(); 
                string currentTarget = mHand.mVisibleHand.searchCollisionRaycastObjectFromIndexPointFingerUpDown(collisionTargets); 
                AdvancedController.selectTarget(currentTarget); 
                AdvancedController.setCurrentHand(mHand.mVisibleHand.getCurrentHand());
                //Debug.Log("currentTarget: " + currentTarget); 
            }
        }
        internal bool isGesturePointUp() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                // 1. Check if index finger is pointing: 
                if(mHand.mState.mJudgeMent.isIndexFingerPlane()) {
                    // 2. Check if index finger points up: 
                    return mHand.mVisibleHand.wherePointsIndexFinger() == Hi5_Hand_Visible_Hand.EPalmOrientation.UP; 
                }
            }
            return false;
        }

        internal bool isGesturePointDown() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                if(mHand.mState.mJudgeMent.isIndexFingerPlane()) {
                    return mHand.mVisibleHand.wherePointsIndexFinger() == Hi5_Hand_Visible_Hand.EPalmOrientation.DOWN;
                }
            }
            return false; 
        }

        internal bool isGesturePointAhead() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                if(mHand.mState.mJudgeMent.isIndexFingerPlane()) {
                    return mHand.mVisibleHand.wherePointsIndexFinger() == Hi5_Hand_Visible_Hand.EPalmOrientation.AHEAD; 
                }
            }
            return false; 
        }
        internal void handlerFistGesture() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                List<string> collisionTargets = AdvancedController.getCollisionTargets(); 
                string currentTarget = mHand.mVisibleHand.searchCollisionRaycastObjectFromFist(collisionTargets); 
                AdvancedController.selectTarget(currentTarget); 
                AdvancedController.setCurrentHand(mHand.mVisibleHand.getCurrentHand());
                //Debug.Log("currentTarget: " + currentTarget); 
                //Debug.Log("gesto puño"); 
            }
        }
        internal void handlerExtendedPalmGesture() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {

                //Debug.Log("Gesto de palma extendida"); 
                
                // Check if finger raycast collides with one chorist
                List<string> collisionTargets = AdvancedController.getCollisionTargets(); 
                //Debug.Log("collisionTargets: " + collisionTargets);
                string currentTarget = mHand.mVisibleHand.searchCollisionRaycastObjectFromPointFinger(collisionTargets); 
                //Debug.Log("currentTarget: " + currentTarget); 
                AdvancedController.selectTarget(currentTarget); 
                AdvancedController.setCurrentHand(mHand.mVisibleHand.getCurrentHand()); 
            }
        }
        internal bool isExtendedPalmUp() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                // 1. Check palm orientation: 
                if(mHand.mVisibleHand.palmOrientation() == Hi5_Hand_Visible_Hand.EPalmOrientation.UP) {
                    // 2. Check all fingers extended: 
                    if(mHand.mState.mJudgeMent.IsFingerPlane()){
                        return true; 
                    }
                }
            }
            return false; 
        }

        internal bool isExtendedPalmDown() {
            if(mHand != null && mHand.mState != null && mHand.mState.mJudgeMent != null) {
                if(mHand.mVisibleHand.palmOrientation() == Hi5_Hand_Visible_Hand.EPalmOrientation.DOWN) {
                    // 2. Check all fingers extended: 
                    if(mHand.mState.mJudgeMent.IsFingerPlane()) {
                        return true; 
                    }
                }
            }
            return false; 
        }

    /* End of modifications in this Script for ChoVR */ 

        
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
        /* Modified for ChoVR */ 
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