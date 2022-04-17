import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService {
    async getAll() {
        const res = await api.get('account/vaults')
        AppState.vaults = res.data
    }
    async addToVault(body) {
        const res = await api.post('api/vaultkeeps', body)
    }
    async getVault(id) {
        const res = await api.get('api/vaults/' + id)
        AppState.activeVault = res.data
    }
    async getVaultKeeps(id) {
        const res = await api.get('api/vaults/' + id + '/keeps')
        AppState.keeps = res.data
    }

}


export const vaultsService = new VaultsService()