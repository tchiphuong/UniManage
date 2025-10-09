import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";

function App() {
    const [count, setCount] = useState(0);

    return (
        <div className="min-h-screen bg-gradient-to-br from-gray-900 to-gray-800 text-white">
            <div className="container mx-auto px-4 py-16">
                <div className="flex flex-col items-center justify-center">
                    <div className="flex gap-8 mb-8">
                        <a href="https://vite.dev" target="_blank" rel="noopener noreferrer">
                            <img
                                src={viteLogo}
                                className="h-24 hover:drop-shadow-[0_0_2em_#646cffaa] transition-all"
                                alt="Vite logo"
                            />
                        </a>
                        <a href="https://react.dev" target="_blank" rel="noopener noreferrer">
                            <img
                                src={reactLogo}
                                className="h-24 hover:drop-shadow-[0_0_2em_#61dafbaa] transition-all animate-spin-slow"
                                alt="React logo"
                            />
                        </a>
                    </div>

                    <h1 className="text-5xl font-bold mb-8 bg-gradient-to-r from-blue-400 to-purple-600 bg-clip-text text-transparent">
                        UniManage Frontend
                    </h1>

                    <div className="bg-gray-800 rounded-lg p-8 shadow-xl mb-8">
                        <button
                            onClick={() => setCount((count) => count + 1)}
                            className="px-8 py-3 bg-blue-600 hover:bg-blue-700 rounded-lg font-semibold transition-colors shadow-lg hover:shadow-blue-500/50"
                        >
                            count is {count}
                        </button>
                        <p className="mt-4 text-gray-300">
                            Edit <code className="bg-gray-700 px-2 py-1 rounded">src/App.tsx</code>{" "}
                            and save to test HMR
                        </p>
                    </div>

                    <p className="text-gray-400">Click on the Vite and React logos to learn more</p>

                    <div className="mt-8 flex gap-4 text-sm text-gray-500">
                        <span>React 18.3+</span>
                        <span>•</span>
                        <span>TypeScript 5.7+</span>
                        <span>•</span>
                        <span>Tailwind CSS 3.4+</span>
                        <span>•</span>
                        <span>Vite 6.0+</span>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;
