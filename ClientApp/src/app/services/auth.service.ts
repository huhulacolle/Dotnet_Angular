import { User, Token, Client } from './../client/clientSwagger';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private userClient: Client
  ) { }

  login(user: User): Promise<Token> {
    return lastValueFrom(this.userClient.login(user));
  }

  register(user: User): Promise<void> {
    return lastValueFrom(this.userClient.register(user))
  }
}
