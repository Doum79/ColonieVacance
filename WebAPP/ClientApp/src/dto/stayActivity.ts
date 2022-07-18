import { Activity } from "./activity";
import { Stay } from "./stay";

export class StayActivity {
    public id: number;
    public stayId: number;
    public stay: Stay;
    public activityId: number;
    public activity: Activity

    constructor(id?: number, stayId?: number, stay?: Stay, activityId?: number, activity?: Activity)

    constructor(id: number, stayId: number, stay: Stay, activityId: number, activity: Activity) {
        this.id = id;
        this.stayId = stayId;
        this.stay = stay;
        this.activityId = activityId;
        this.activity = activity;
    }
}
