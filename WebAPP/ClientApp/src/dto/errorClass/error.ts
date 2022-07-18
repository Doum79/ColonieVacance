// Code Error
// -1 : Error
// 0 : Information
// 1 : Success

export class ErrorMessage {
	public codeError: number;
	public label: string;

	constructor(codeError?: number, label?: string)

	constructor(codeError: number, label: string) {
		this.codeError = codeError;
		this.label = label;
	}
}