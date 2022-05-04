import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {
  BuildVersion,
  BuildVersionsClient,
} from '../services/BuildVersionsClient';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  public versions?: Observable<BuildVersion[]>;

  constructor(private client: BuildVersionsClient) {}

  ngOnInit(): void {
    this.versions = this.client.get();
  }
}
