import { Injectable } from '@angular/core';

@Injectable()
export class Configuration {
    private server = 'http://localhost:5000/';
    private apiPath = 'api/';
    public apiUrl = this.server + this.apiPath;
}