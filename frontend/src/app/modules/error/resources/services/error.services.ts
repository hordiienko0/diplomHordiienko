import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HttError } from "../models/httpError";

@Injectable({
    providedIn: "root",
})
export class ErrorService {

    getErrorMessage(error: HttError,message:string) {
        if (error !== null) {
            return error.detail
        }
        return message
    }
}
