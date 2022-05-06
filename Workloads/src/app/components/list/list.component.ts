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
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
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
