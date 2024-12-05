export default interface Visit {
    id: string;
    country: string;
    emoji: string | null;
    currency: string | null;
    currencyName: string | null;
    symbol: string | null;
    latitude: number | null;
    longitude: number | null;
    visitDate: string;
}