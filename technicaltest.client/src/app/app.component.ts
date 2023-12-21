import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ViewChild } from '@angular/core';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  csvRecords: any[] = [];
  header: boolean = true;

  constructor(private ngxCsvParser: NgxCsvParser, private http: HttpClient) { } 

  @ViewChild('fileImportInput') fileImportInput: any;

  fileChangeListener($event: any): void {
    const apiUrl = 'http://localhost:5014/examScore';
    const files = $event.srcElement.files;
    this.header = true;

    const fileReader = new FileReader();
    fileReader.onload = (e) => {
      const csvData = fileReader.result as string;
      const modifiedCsvData = this.trimHeaderSpaces(csvData);
      const modifiedCsvFile = new File([modifiedCsvData], files[0].name); // Create a new File object

      this.ngxCsvParser.parse(modifiedCsvFile, { header: this.header, delimiter: ',', encoding: 'utf8' })
        .pipe()
        .subscribe(
          (result: any[] | NgxCSVParserError) => {

            if (result instanceof Array) {
              console.log('Result',  result);

              this.http.post(apiUrl, result, {
                headers: {
                  'Content-Type': 'application/json'
                }
              })
                .subscribe(
                  (response) => {
                    this.csvRecords = response as any;
                  },
                  (error) => {
                    // Handle error
                    console.log("error", error)
                  }
                );

            }

            else {
              console.error("Error", NgxCSVParserError)
            }
          }
        );
    };

    fileReader.readAsText(files[0]);
  }

  trimHeaderSpaces(csvData: string): string {
    const lines = csvData.split('\n');
    const headers = lines[0].split(',');

    const trimmedHeaders = headers.map(header => header.replace(" ", ""));
    lines[0] = trimmedHeaders.join(',');

    return lines.join('\n');
  }
}
  


