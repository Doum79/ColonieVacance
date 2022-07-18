export class StayPicture {
    public id: number;
    public stayId: number;
    public pictureUrl: string;
    public pictureName: string;

    constructor(id?: number, stayId?: number, pictureUrl?: string, pictureName?: string)

    constructor(id: number, stayId: number, pictureUrl: string, pictureName: string) {
        this.id = id;
        this.stayId = stayId;
        this.pictureUrl = pictureUrl;
        this.pictureName = pictureName;
    }
}
