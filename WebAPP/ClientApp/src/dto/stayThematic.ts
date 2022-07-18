import { Stay } from "./stay";
import { Thematic } from "./thematic";

export class StayThematic {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public thematicId: number;
    public thematic: Thematic

    constructor(id?: number, stayId?: number, stay?: Stay, thematicId?: number, thematic?: Thematic)

    constructor(id: number, stayId: number, stay: Stay, thematicId: number, thematic: Thematic) {
        this.id = id;
        this.stayId = stayId;
        this.stay = stay;
        this.thematicId = thematicId;
        this.thematic = thematic;
    }
}
