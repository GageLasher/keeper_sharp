import { AppState } from "../AppState"
import { api } from "./AxiosService"



class KeepsService {
    async getAll() {
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
    }
    async setActive(keep) {
        const res = await api.get('api/keeps/' + keep.id)
        AppState.activeKeep = res.data

    }

}


export const keepsService = new KeepsService()