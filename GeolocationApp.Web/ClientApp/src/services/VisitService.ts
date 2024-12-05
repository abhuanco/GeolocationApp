import HttpClientVisit from "@api/HttpClientVisit.ts";
import Visit from "@models/Visit.ts";
import PaginationVisit from "@models/PaginationVisit.ts";

export default class VisitService {

    constructor(private readonly httpClientVisit: HttpClientVisit) {
    }

    public async tracking(): Promise<Visit> {
        return await this.httpClientVisit.post('/api/Visit', {})
    }

    public async getVisitById(id: string): Promise<Visit> {
        return await this.httpClientVisit.get(`/api/Visits/${id}`, {});
    }

    public async getVisits(): Promise<PaginationVisit> {
        return await this.httpClientVisit.all(`/api/Visit`);
    }
}