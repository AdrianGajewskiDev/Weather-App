import HttpStatusCode from "../statusCode";

export declare class Response<T> {
  responseBody: T;
  statusCode: HttpStatusCode;
}
