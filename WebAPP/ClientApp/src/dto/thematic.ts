﻿export class Thematic {
    public id: number;
    public label: string;

    constructor(id?: number, label?: string)

    constructor(id: number, label: string) {
        this.id = id;
        this.label = label;
    }
}
