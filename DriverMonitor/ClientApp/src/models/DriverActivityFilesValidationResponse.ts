import { DriverActivityFile } from "./DriverActivityFile";

export interface DriverActivityFilesValidationResponse
{
    isValidSimulation : boolean,
    validationFailureType : number,
    driverActivityFiles : DriverActivityFile[]
}
