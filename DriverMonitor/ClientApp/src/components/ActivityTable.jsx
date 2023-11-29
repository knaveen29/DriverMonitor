import { Table } from "react-bootstrap";
import { React } from "react";
import TableRow from "./TableRow.jsx";
import { TABLE_HEADERS } from "../constants/helper.ts";

const ActivityTable = ({ type, driversActivity, title }) => {
  console.log(driversActivity);
  return (
    <div className="p-3">
      <h5>{title}</h5>
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>#</th>
            <th>{TABLE_HEADERS.DRIVER_ID}</th>
            <th>{TABLE_HEADERS.ACTIVITY}</th>
            <th>{TABLE_HEADERS.ACTIVITY_DATE}</th>
            <th>{TABLE_HEADERS.START_DATE}</th>
            <th>{TABLE_HEADERS.END_DATE}</th>
          </tr>
        </thead>
        <tbody>
          {driversActivity
            ?.filter((item) => item.type === type)[0]
            ?.activity.map((item, ind) => (
              <TableRow key={ind} props={item} />
            ))}
        </tbody>
      </Table>
    </div>
  );
};

export default ActivityTable;
