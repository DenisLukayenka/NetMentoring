import { IResponse } from './IResponse';

export class StatusResponse implements IResponse {
    public isSuccess: boolean;
}