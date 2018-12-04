// import React from 'react';
// import { Pagination } from 'semantic-ui-react';
//
// export default class PaginationContainer extends React.Component {
//     render() {
//         return (
//             <Pagination
//                 defaultActivePage={1}
//                 firstItem={null}
//                 lastItem={null}
//                 pointing
//                 secondary
//                 totalPages={this.props.pageNumber}>
//             </Pagination>
//         );
//     }
// }

import React from 'react';
import { Pagination } from 'semantic-ui-react';

class PaginationContainer extends React.Component {
    constructor(props) {
        super(props);

        const { itemList } = this.props;
        console.log(itemList);

        this.state = { startCnt: 0, li: [] };
        this.nextPage = this.nextPage.bind(this);
        this.lastPage = this.lastPage.bind(this);
    }

    renderList() {
        const { itemList } = this.props;
        console.log(itemList);

        const li = [];

        for (let i = this.state.startCnt; i < this.state.startCnt + 3; i++) {
            li.push(<li>{itemList[i].title}</li>)
        }

        return li;
    }

    nextPage() {
        const a = this.state.startCnt;
        if (a + 3 < 9) { this.setState({ startCnt: a + 3}); }
    }

    lastPage() {
        const a = this.state.startCnt;
        if (a - 3 >= 0) { this.setState({ startCnt: a - 3}); }
    }

    render() {
        return (
            <div className="PaginationContainer">
                <ul>
                    { this.renderList() }
                </ul>
                <button onClick={this.lastPage}>last</button>
                <button onClick={this.nextPage}>next</button>
            </div>
        );
    }
}

export default PaginationContainer;