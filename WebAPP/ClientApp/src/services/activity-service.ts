import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { CreationObject } from "../shared/functions/creation-object";
import { Activity } from "../dto/activity";

@Injectable()
export class ActivityService extends HttpRequestMethods {

    async listActivity() {
        var rslt = await this.get("activity");
        var rsltTab = rslt as string[];

        var activityList = new Array<Activity>();
        rsltTab.forEach(activity => {
            activityList.push(CreationObject.CreateActivity(activity));
        })
        return activityList;
    } 
}