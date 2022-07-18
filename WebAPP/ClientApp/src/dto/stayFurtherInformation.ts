import { Stay } from "./stay";

export class StayFurtherInformation {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public startDate: Date;
    public endDate: Date;
    public withTransport: boolean;
    public startCity: string;
    public price: number;
    public redirectionLink: string; 
    
    constructor(id?: number, stayId?: number, stay?:Stay, startDate?: Date, endDate?: Date,
        withTransport?: boolean, startCity?: string,
        price?: number, redirectionLink?: string)

    constructor(id: number, stayId: number, stay:Stay, startDate: Date, endDate: Date,
        withTransport: boolean, startCity: string,
        price: number, redirectionLink: string) {
            this.id = id;
            this.stayId = stayId;
            this.stay = stay;
            this.startDate = startDate;
            this.endDate = endDate;
            this.withTransport = withTransport;
            this.startCity = startCity;
            this.price = price;
            this.redirectionLink = redirectionLink;
    }
}
