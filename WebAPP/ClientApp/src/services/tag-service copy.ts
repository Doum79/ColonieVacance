import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { CreationObject } from "../shared/functions/creation-object";
import { Tag } from "src/dto/tag";

@Injectable()
export class TagService extends HttpRequestMethods {

    async listTag() {
        var rslt = await this.get("tag");
        var rsltTab = rslt as string[];

        var tagsList = new Array<Tag>();
        rsltTab.forEach(tag => {
            tagsList.push(CreationObject.CreateTag(tag));
        })
        return tagsList;
    }
}