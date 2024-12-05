import HttpClient from "@api/HttpClient.ts";
import Visit from "@models/Visit.ts";
import PaginationVisit from "@models/PaginationVisit.ts";

export default class HttpClientVisit extends HttpClient<Visit, PaginationVisit> {
    constructor() {
        super();
    }
    
}