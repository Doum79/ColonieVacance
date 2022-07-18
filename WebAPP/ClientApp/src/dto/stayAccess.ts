import { Stay } from "./stay";

export class StayAccess {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public label: string;

    constructor(id?: number, stayId?: number, stay?: Stay, label?: string)

    constructor(id: number, stayId: number, stay: Stay, label: string) {
        this.id = id;
        this.stayId = stayId;
        this.stay = stay;
        this.label = label;
    }
}
