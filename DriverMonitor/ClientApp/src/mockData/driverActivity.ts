import { DriverActivityResponse } from "../models/DriverActivityResponse";

const details: DriverActivityResponse = {
  violations: [
    {
      type: 1, //single drive violation
      activity: [
        {
          id: 1,
          activity: "drive",
          driverId: 123,
          startTime: new Date("2023-02-02 01:00"),
          endTime: new Date("2023-02-02 10:00"),
          createTime: new Date("2023-02-02"),
        },
        {
          id: 2,
          activity: "drive",
          driverId: 231,
          startTime: new Date("2023-02-03 14:00"),
          endTime: new Date("2023-02-03 22:00"),
          createTime: new Date("2023-02-03"),
        },
      ],
    },
    {
      type: 2, //day drive violation
      activity: [
        {
          id: 3,
          activity: "drive",
          driverId: 123,
          startTime: new Date("2023-02-02"),
          endTime: new Date("2023-02-03"),
          createTime: new Date("2023-02-02"),
        },
        {
          id: 4,
          activity: "drive",
          driverId: 231,
          startTime: new Date("2023-02-03"),
          endTime: new Date("2023-02-04"),
          createTime: new Date("2023-02-02"),
        },
      ],
    },
  ],
};

export default details;
