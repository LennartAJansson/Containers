import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {
  BuildVersion,
  Binary,
  BuildVersionsClient,
} from '../../services/BuildVersionsClient';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  public versionsObservable?: Observable<Binary[]>;
  public versions?: Binary[];

  constructor(private client: BuildVersionsClient) {}

  ngOnInit(): void {
    this.versionsObservable = this.client.getBinaries();
    this.versionsObservable.pipe().subscribe((obj) => {
      return (this.versions = obj);
    });
  }
}
