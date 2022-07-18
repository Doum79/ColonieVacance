import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { CreationObject } from "../shared/functions/creation-object";
import { Tag } from "src/dto/tag";

@Injectable()
export class TagStayService extends HttpRequestMethods {

    async listTagsOfStay(stayId: number) {
        var rslt = await this.get(`staytag/stay/${stayId}`);
        var rsltTab = rslt as string[];

        var tagsList = new Array<Tag>();
        rsltTab.forEach(tag => {
            tagsList.push(CreationObject.CreateTag(tag));
        })
        return tagsList;
    }
}