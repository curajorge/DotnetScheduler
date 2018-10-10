using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public enum ScheduleType
    {
        Work = 1,
        Coffee = 2,
        Doctor = 3,
        Shopping = 4,
        Other = 5
    }

    public enum LeadType
    {
        
        AmericaTv = 1,
        Amor107 = 2,
        CaracolRadio = 3,
        CornerstoneDirectMail = 4,
        CornerstoneOrganic = 5,
        CornestonePPC = 6,
        CornerstoneSocialMedia = 7,
        EnterconLeads = 8,
        FMwebsite = 9,
        GooglePlaces = 10,
        MarlonTavera = 11,
        Power96 = 12,
        ReinerLead = 13,
        RenewFinancial = 14,
        RITMO95 = 15,
        TeveoAmericaTV = 16,
        Yelp = 17,
        YGRENE = 18,
        ZETA92 = 19,
        ZOL106 = 20
        

    }
    public enum ScheduleStatus
    {
        Valid = 1,
        Cancelled = 2
    }

    public enum OpportunityType
    {
        type1 = 1,
        type2 = 2
    }
    public enum JobType
    {
        CallBack = 1,
        CodExisting = 2,
        CodNew = 3,
        Warranty = 4,
        FollowUp = 5,
        TuneUp = 6,
        Estimate = 7,
        Installation = 8
        
    }
    public enum UserType
    {
        Admin = 0,
         Sales= 1,
        Tech = 2,
        CSR = 3
    }
    public enum UserStatusType
    {
        type1 = 1,
        type2 = 2
    }
    public enum AddressType
    {
        type1 = 1,
        type2 = 2
    }
    public enum JobStatusType
    {
        Active = 1,
        Inactive = 0
    }
    public enum WorkType
    {
        Estiamte = 1,
        DemandService = 2,
        MaintenanceAgreement = 3,
        TuneUp = 4,
        WarrantyCallBacks = 5
    }
}
