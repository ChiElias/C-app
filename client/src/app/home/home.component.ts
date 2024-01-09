import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  regisMode = false
  users: any
  constructor(private http: HttpClient) { }
  regisToggle() {
    this.regisMode = !this.regisMode
  }
  getUsers() {
    this.http.get('https://localhost:7777/api/users').subscribe({
      next: (response) => this.users = response,
      error: (err) => console.log(err),
      complete: () => console.log('request completed')
    })
  }
  ngOnInit(): void {
    //this.getUsers()
  }
  cancelRegister(event: boolean) {
    this.regisMode = !event
  }
}
