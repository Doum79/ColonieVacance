export class DConnexion {
	public profil: boolean;
	public email: string;
	public password: string;

	constructor(profil?: boolean, email?: string, password?: string)

	constructor(profil: boolean, email: string, password: string) {
		this.profil = profil;
		this.email = email;
		this.password = password;
	}
}