using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    class DesignDocument_Template
    {

        ////Workflow - Design Document - Comment Review////

        ////Create Document
        //login as testiqfuser <SG&SH249 = TestIQFRecordsManager>
        //@@Nav to RMCenter DesignDocument
        //@@click Create Design doc button
        //@@enter required fields (title, doc#, select file )    
        //@@click save&fwd button
        //@@logout

        ////Make comment as DOT user
        //login as TestDOTuser
        //@@Nav to RMCenter DesignDocument
        //@@under DOT Pending Comment tab, find record and click Enter button
        //wait for comment section to load
        //select Review type = Regular Comment (alt: No Comment - record shows in DEV Closed tab)
        //enter comment
        //enter drawing/page number
        //click SaveOnly button
        //logout

        ////Forward comment as DOTAdmin
        //login as TestDOTAdmin
        //Nav to RMCenter DesignDocument
        //under DOT Pending Comment tab, find record and click Enter button
        //wait for comment section to load
        //click SaveForward button
        //logout

        ////Make Response comment as Dev User
        //login as TestDevUser
        //Nav to RMCenter DesignDocument
        //under DEV Requires Response tab(switch to tab), find record and click Enter button
        //wait for comment section to load
        //enter Response comment
        //enter Response code (DDL) = Disagree (alt: Agree or Clarification(?) - no need to resolution step, loginAs DevAdmin, SaveFwd then verify in DEV Closed tab)
        //click SaveOnly button
        //logout

        ////Forward Response comment as Dev Admin
        //login as TestDevAdmin
        //Nav to RMCenter DesignDocument
        //under DEV Requires Response tab(switch to tab), find record and click Enter button
        //wait for comment section to load
        //click SaveForward button

        ////Make Resolution Comment as Dev Admin
        //switch tab to DEV Requires Resolution, find record and click Enter button
        //wait for comment section to load
        //enter resolution comment
        //select Resolution Stamp = Agree on Response
        //click SaveForward button
        //switch tab to DEV Closed and verify record is present
        //logout
    }
}
