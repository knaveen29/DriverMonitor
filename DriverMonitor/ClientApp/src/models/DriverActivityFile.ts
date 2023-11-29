export interface DriverActivityFile {
    id?: number;
    driverId: number;
    activityType: string;
    startTime: Date;
    endTime: Date;
    createTime: Date;
}

export interface DriverActivityFileRequest {
    Id?: number;
    DriverId: number;
    ActivityType: string;
    StartTime: Date;
    EndTime: Date;
    CreateTime: Date;
}