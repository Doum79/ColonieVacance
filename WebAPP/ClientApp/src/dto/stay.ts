import { StayAccess } from "./stayAccess";
import { StayActivity } from "./stayActivity";
import { StayEquipment } from "./stayEquipment";
import { StayFurtherInformation } from "./stayFurtherInformation";
import { StayPicture } from "./stayPicture";
import { StayTeam } from "./stayTeam";
import { StayThematic } from "./stayThematic";

export class Stay {
    public id: number;
    public structureId: number;
    public title: string;
    public minYear: number;
    public maxYear: number;
    public abstract: string;
    public description: string;
    public program: string;
    public createdDate: Date;
    public housing: string;
    public moreInformations: string;
    public street: string;
    public postCode: string;
    public city: string;
    public state: string;
    public country: string;
    public latitude: string;
    public longitude: string;
    public phone: number;
    public partnersList: Array<StayTeam>;
    public equipmentsList: Array<StayEquipment>;
    public accessesList: Array<StayAccess>;
    public furtherInformationsList: Array<StayFurtherInformation>;
    public picturesList: Array<StayPicture>;
    public activitiesList: Array<StayActivity>;
    public thematicsList: Array<StayThematic>;

    constructor(id?: number, structureId?: number, title?: string, minYear?: number, maxYear?: number, abstract?: string,
        description?: string, program?: string, createdDate?: Date, housing?: string, moreInformations?: string, 
        street?: string, postCode?: string, city?: string, state?: string, country?: string, latitude?: string, 
        longitude?: string, phone?: number, partnersList?: Array<StayTeam>, equipmentsList?: Array<StayEquipment>, 
        accessesList?: Array<StayAccess>, furtherInformationsList?: Array<StayFurtherInformation>, 
        picturesList?: Array<StayPicture>, activitiesList?: Array<StayActivity>, thematicsList?: Array<StayThematic>)

    constructor(id: number, structureId: number, title: string, minYear: number, maxYear: number, abstract: string,
        description: string, program: string, createdDate: Date, housing: string, moreInformations: string, 
        street: string, postCode: string, city: string, state: string, country: string, latitude: string, 
        longitude: string, phone: number, partnersList: Array<StayTeam>, equipmentsList: Array<StayEquipment>, 
        accessesList: Array<StayAccess>, furtherInformationsList: Array<StayFurtherInformation>, 
        picturesList: Array<StayPicture>, activitiesList: Array<StayActivity>, thematicsList: Array<StayThematic>) {
        this.id = id;
        this.structureId = structureId;
        this.title = title;
        this.minYear = minYear;
        this.maxYear = maxYear;
        this.abstract = abstract;
        this.description = description;
        this.program = program;
        this.createdDate = createdDate;
        this.housing = housing;
        this.moreInformations = moreInformations;
        this.street = street;
        this.postCode = postCode;
        this.city = city;
        this.state = state;
        this.country = country;
        this.latitude = latitude;
        this.longitude = longitude;
        this.phone = phone;
        this.partnersList = partnersList;
        this.equipmentsList = equipmentsList;
        this.accessesList = accessesList;
        this.furtherInformationsList = furtherInformationsList;
        this.picturesList = picturesList;
        this.activitiesList = activitiesList;
        this.thematicsList = thematicsList;
    }
}
