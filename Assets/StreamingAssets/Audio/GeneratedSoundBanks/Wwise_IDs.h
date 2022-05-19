/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_MAINBGM = 1691765453U;
        static const AkUniqueID SET_STATE_FALLINGDOWN = 1444949965U;
        static const AkUniqueID SET_STATE_FALLINGLIGHT = 2258315847U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace FALLINGDOWN
        {
            static const AkUniqueID GROUP = 3934905550U;

            namespace STATE
            {
                static const AkUniqueID DISABLED = 3248502869U;
                static const AkUniqueID ENABLED = 4174102348U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace FALLINGDOWN

        namespace FALLINGLIGHT
        {
            static const AkUniqueID GROUP = 746704402U;

            namespace STATE
            {
                static const AkUniqueID DISABLED = 3248502869U;
                static const AkUniqueID ENABLED = 4174102348U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace FALLINGLIGHT

        namespace LEVELGROUP
        {
            static const AkUniqueID GROUP = 1315568674U;

            namespace STATE
            {
                static const AkUniqueID L0_BEGIN = 819628587U;
                static const AkUniqueID L1_HELP = 1241384302U;
                static const AkUniqueID L2_RAININGBREAD = 3534658058U;
                static const AkUniqueID L3_FALLING = 2711863090U;
                static const AkUniqueID L4_WEEPINGPOTATOES = 2259867596U;
                static const AkUniqueID L5_OLDMANRING = 1777991132U;
                static const AkUniqueID L6_HB = 2347353944U;
                static const AkUniqueID L7_MYSELF = 2227088069U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace LEVELGROUP

    } // namespace STATES

    namespace TRIGGERS
    {
        static const AkUniqueID FALLINGDOWNREADY = 778577675U;
        static const AkUniqueID FALLINGLIGHTREADY = 1834139223U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINBANK = 2880737896U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
