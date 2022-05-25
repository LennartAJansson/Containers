import {LiveAnnouncer} from '@angular/cdk/a11y';
import { 
  Component,
  // OnInit,
  ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs';
// import { Observable } from 'rxjs';
import {
  // BuildVersion,
  Binary,
  BuildVersionsClient,
} from '../../services/BuildVersionsClient';

export class FlatBinary {
  projectFile: string|undefined;
  semanticVersion: string|undefined;
  semanticRelease: string|undefined;
  /**
   *
   */
  constructor(projectFile: string, semanticVersion: string, semanticRelease: string) {
    this.projectFile=projectFile;
    this.semanticRelease=semanticRelease;
    this.semanticVersion=semanticVersion;
  }
}

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})

// export class ListComponent implements OnInit {
export class ListComponent {
  // public versionsObservable?: Observable<Binary[]>;
  
  public versions?: FlatBinary[];
  dataSource = new MatTableDataSource(this.versions);

  displayedColumns = ["projectFile", "semanticVersion", "semanticRelease"];
  @ViewChild(MatSort) sort?: MatSort;
  
  constructor(private client: BuildVersionsClient,private _liveAnnouncer: LiveAnnouncer) {}

  ngOnInit(): void {
    console.log("ngOnInit");
    // this.versionsObservable = this.client.getBinaries();
    this.client.getBinaries()
      .pipe(map( binaries => binaries.map(binary => new FlatBinary(binary.projectFile!, binary.buildVersion?.semanticRelease!, binary.buildVersion?.semanticVersion!))))
      .subscribe((obj) => {
        this.versions = obj;
        this.dataSource = new MatTableDataSource(this.versions)
        return (obj);
      });
  }

  ngAfterViewInit() {
    console.log("ngAfterViewInit");
    // this.client.getBinaries()
    //   .pipe()
    //   .subscribe((obj) => {
    //     this.versions = obj;
    //     this.dataSource = new MatTableDataSource(this.versions!)
    //     return (obj);
    //   });
      this.dataSource.sort = this.sort!;
    }

    announceSortChange(sortState: Sort) {
      // This example uses English messages. If your application supports
      // multiple language, you would internationalize these strings.
      // Furthermore, you can customize the message to add additional
      // details about the values being sorted.
      console.log("announceSortChange");

      if (sortState.direction) {
        this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
      } else {
        this._liveAnnouncer.announce('Sorting cleared');
      }
    }  }
