import React from "react";
import { post } from "../services/api";
import { apiUrl } from "../services/apiUrl";
// ErrorBoundary�H����child element���o�Ϳ��~���|�Q����
// �H�U�|�ر��p���|�Q����
// Event handlers�B�D�P�B���{���X (�Ҧp setTimeout �� requestAnimationFrame callback)
// Server side rendering�B�b���~��ɸ̥�X�����~�]�Ӥ��O�b�����U�� children�^
class ErrorBoundary extends React.Component {
    constructor(props) {
        super(props);
        this.state = { hasError: false };
    }
    //��l�ե�o�Ϳ��~��Ĳ�o�A�Ψӧ�sUI
    static getDerivedStateFromError(error) {
        // ��s state �H�ܩ�U�@�� render �|��� fallback UI
        return { hasError: true };
    }
    //�������~�M���~��T
    componentDidCatch(error, errorInfo) {
        post(apiUrl.errorLog, {
            error: error.toString(),
            errorInfo: errorInfo.componentStack,
        }).catch((e) => {
            console.error("Error logging failed", e);
        });
        console.log('componentDidCatch', { error, errorInfo });
    }
    //���Ѵ��N��UI
    render() {
        if (this.state.hasError) {
            return (
                <div style={{ padding: "20px", textAlign: "center" }}>
                    <h1 style={{ color: "red" }}>Oops! Something went wrong.</h1>
                    <p>We're sorry for the inconvenience. Please try again later.</p>
                    <button onClick={() => window.location.reload()} style={{ padding: "10px 20px", fontSize: "16px" }}>
                        Reload Page
                    </button>
                </div>
            );
        }

        return this.props.children;
    }
}

export default ErrorBoundary;