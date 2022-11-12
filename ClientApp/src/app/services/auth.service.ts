import { User, UserClient, FileResponse, Token } from './../client/clientSwagger';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private userClient: UserClient
  ) { }

  login(user: User): Promise<Token> {
    return lastValueFrom(this.userClient.login(user));
  }

  register(user: User): Promise<FileResponse | null> {
    return lastValueFrom(this.userClient.register(user))
  }
}
