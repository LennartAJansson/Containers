import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {
  WorkloadsClient,
  CommandAssignmentResponse,
  CommandPersonResponse,
  CommandWorkloadResponse,
  QueryAssignmentResponse,
  QueryPersonResponse,
  QueryWorkloadResponse,
  CommandCreateAssignment,
  CommandCreatePerson,
  CommandCreateWorkload,
  CommandUpdateAssignment,
  CommandUpdatePerson,
  CommandUpdateWorkload,
  ProblemDetails,
  ApiException,
} from '../../services/WorkloadsClient';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Workloads';
  public workloadsObservable?: Observable<QueryWorkloadResponse[]>;
  public workloads?: QueryWorkloadResponse[];

  constructor(private client: WorkloadsClient) {}

  ngOnInit(): void {
    this.workloadsObservable = this.client.getWorkloads();
    this.workloadsObservable.pipe().subscribe((obj) => {
      return (this.workloads = obj);
    });
  }
}
