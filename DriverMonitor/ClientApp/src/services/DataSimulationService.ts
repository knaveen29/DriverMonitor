import {
  DriverActivityFileRequest,
} from "../models/DriverActivityFile";
import { driverActivityApi } from "../api/DriverActivityApi.ts";
import { ACTIVITY_TYPE } from "../constants/helper.ts";

export const generateSampleData = (numDrivers) => {
  console.log("Generating sample data for drivers : " + numDrivers);

  const driverFiles: DriverActivityFileRequest[] = [];
  for (let i = 1; i <= numDrivers; i++) {
    driverFiles.push(...generateDriverFiles(i));
  }
  console.log("Generating sample data completed!");
  return driverFiles;
};

export const fetchDriverActivityData = async (
  startDate: Date,
  endDate: Date
) => {
  console.log("Fetching Driver activity data - Begin");

  const result = await driverActivityApi
    .getActivityViolationData(startDate, endDate)
    .then((response) => {
      return response;
    });
  
  console.log("Fetching Driver activity data - Complete");
  return result;
};

export const getValidationResult = async (driverFiles) => {
  const result = await driverActivityApi.validateActivityData(driverFiles);
  return result;
};

export const saveFileData = async (driverFiles) => {
  const result = await driverActivityApi.postData(driverFiles);
  return result;
};

const generateDriverFiles = (driverId) => {
  const numTrips = Math.floor(Math.random() * 5) + 1; // Generate 1 to 5 trips
  const driverFiles: DriverActivityFileRequest[] = [];
  let lastTripEndDt = new Date();
  for (let i = 0; i < numTrips; i++) {
    const startDatetime = getRandomDatetime(lastTripEndDt);
    const endDatetime = getRandomDatetime(startDatetime);

    const driverActivity: DriverActivityFileRequest = {
      Activity: ACTIVITY_TYPE[Math.floor(Math.random() * ACTIVITY_TYPE.length)],
      CreateTime: new Date(),
      StartTime: startDatetime,
      EndTime: endDatetime,
      DriverId: driverId,
    };
    lastTripEndDt = endDatetime;
    driverFiles.push(driverActivity);
  }

  return driverFiles;
};

const getRandomDatetime = (startDatetime = new Date(2023, 0, 1)) => {
  const startTimestamp = startDatetime.getTime();
  const endTimestamp =
    startTimestamp + Math.floor(Math.random() * 4 * 60 * 60 * 1000); // Up to 4 hours

  return new Date(
    startTimestamp + Math.floor(Math.random() * (endTimestamp - startTimestamp))
  );
};
