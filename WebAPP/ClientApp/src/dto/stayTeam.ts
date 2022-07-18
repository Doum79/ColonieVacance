import { Stay } from "./stay";

export class StayTeam {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public partnerName: string;

    constructor(id?: number, stayId?: number, stay?: Stay, partnerName?: string)

    constructor(id: number, stayId: number, stay: Stay, partnerName: string) {
        this.id = id;
        this.stayId = stayId;
        this.stay = stay;
        this.partnerName = partnerName;
    }
}
