import { Stay } from "./stay";

export class StayEquipment {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public label: string;
    public isIncluded: boolean

    constructor(id?: number, stayId?: number, stay?: Stay, label?: string, isIncluded?: boolean)

    constructor(id: number, stayId: number, stay: Stay, label: string, isIncluded: boolean) {
        this.id = id;
        this.stayId = stayId;
        this.stay = stay;
        this.label = label;
        this.isIncluded = isIncluded;
    }
}
