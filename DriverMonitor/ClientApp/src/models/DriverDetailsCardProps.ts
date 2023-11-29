import { DriverActivityFile } from "./DriverActivityFile";

export interface DriverDetailsCardProps {
    cardType: string;
    driverDetails: DriverActivityFile[];
    onClickCancel: () => void;
    onClickUpdate: () => void;
  }