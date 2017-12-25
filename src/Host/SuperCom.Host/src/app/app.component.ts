import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from "./user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  users: Array<User> = [];
  selectedUser: User;
  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
      
    this.http.get('api/user').subscribe(data => {
     this.users = <Array<User>>data;

     console.log(this.users);
    });
  }

  selectUser(user: User) {
    this.selectedUser = user;
  }
}
