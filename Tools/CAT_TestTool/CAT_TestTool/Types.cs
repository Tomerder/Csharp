using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CAT_TestTool
{
    class Types
    {

        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct ExtPcDataOutStruct_EPCManager
        {
            public int iMessageIdFeedback;
            RequestedAlignmentCommandEnum_EPCManager eRequestedAlignCommand;
            public int iLateralAlignValue;
            public int iVerticalAlignValue;
            public int iRollAlignValue;
            public int iCalculatedCrc;
            VideoStateEnum eEvsMode;
            public int iApmLateralAlign;
            public int iApmVerticallAlign;
            public int iApmRollAlign;
            public int iApmAlignCrc;
            EvsGoNoGoEnumAlignFeedbackEnum_EPCManager eEvsLosDev;
            public fixed byte aAcType[16];
            public int iSerialNumber;
            public fixed byte aHudAlignMessage[256];

            static public ExtPcDataOutStruct_EPCManager Deserialize(byte[] data)
            {
                fixed (byte* pData = &data[0])
                {
                    return *(ExtPcDataOutStruct_EPCManager*)pData;
                }
            }
        }
/*
        public struct ExtPcDataOutStruct_EPCManager
        {
              int iMessageIdFeedback;
              RequestedAlignmentCommandEnum_EPCManager eRequestedAlignCommand;
              int iLateralAlignValue;
              int iVerticalAlignValue;
              int iRollAlignValue;
              int iCalculatedCrc;
              VideoStateEnum eEvsMode;
              int iApmLateralAlign;
              int iApmVerticallAlign;
              int iApmRollAlign;
              int iApmAlignCrc;
              EvsGoNoGoEnumAlignFeedbackEnum_EPCManager eEvsLosDev;
              char[] aAcType = new char[16];
              int iSerialNumber;
              char[]  aHudAlignMessage = new char[256];
       };
*/
       

        enum RequestedAlignmentCommandEnum_EPCManager{
          ALIGN_CMD_HUD_ALIGNMENT_EPCManager,
          ALIGN_CMD_EVS_MANUAL_ALIGNMENT_EPCManager,
          ALIGN_CMD_EVS_AUTO_ALIGNMENT_EPCManager,
          ALIGN_CMD_CTVS_ALIGNMENT_EPCManager,
          ALIGN_CMD_HUD_VERIFY_EPCManager,
          ALIGN_CMD_EVS_VERIFY_EPCManager,
          ALIGN_CMD_CTVS_VERIFY_EPCManager,
          ALIGN_CMD_EXIT_ALIGNMENT_EPCManager,
          ALIGN_CMD_REMOVE_EVS_CROSS_EPCManager,
          ALIGN_CMD_HUD_ESTABLISH_CONNECTION_EPCManager,
          ALIGN_CMD_HUD_VERIFY_REQUESTED_ACTIVITIY_ERROR_EPCManager
       };

        enum VideoStateEnum { VIDEO_STATE_OFF, VIDEO_STATE_ON } ;

        enum EvsGoNoGoEnumAlignFeedbackEnum_EPCManager
        {
              Default_EPCManager = 0,
              GoState_EPCManager = 1,
              NoGoState_EPCManager = 2
        };

    }
}
