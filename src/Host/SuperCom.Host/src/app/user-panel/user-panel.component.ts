import { Component, OnInit, Input  } from '@angular/core';
import { User } from "../user";

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {
  @Input() user: User;
  @Input() clickevent: Function;
  constructor() { }

  ngOnInit() {
  }

  click() : void
  {
    this.clickevent(this.user);
  }
}
