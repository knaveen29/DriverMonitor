import { DriverActivityFile } from "./DriverActivityFile";

export interface DriverActivityResponse {
    violations: DriverActivityViolation[]
}

export interface DriverActivityViolation
{
    type: number,
    activity: DriverActivityFile[]
}
