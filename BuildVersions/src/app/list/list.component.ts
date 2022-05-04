import { Component, OnInit } from '@angular/core';
import { async, Observable } from 'rxjs';
import {
  BuildVersion,
  Binary,
  BuildVersionsClient,
} from '../services/BuildVersionsClient';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  public versionsObs?: Observable<Binary[]>;
  public versions?: Binary[];

  constructor(private client: BuildVersionsClient) {}

  ngOnInit(): void {
    this.versionsObs = this.client.getBinaries();
    this.versionsObs.pipe().subscribe((obj) => {
      return (this.versions = obj);
    });
  }
}
